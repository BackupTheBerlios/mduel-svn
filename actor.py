####
#
#class StrictList (list):
#	type = None
#	def append (self, element):
#		assert isinstance (element, self.type)
#		list.append (self, element)
#	
#	def insert (self, index, element):
#		assert isinstance (element, self.type)
#		list.append (self, element)
		
#class Node(object):
#	__children = StrictList(Node)
#
#	children = property (lambda self: self.__children)
	
import pygame, os, pygame.draw, pygame.color
from pygame.locals import *
from pygame.sprite import Sprite

def load_image(name):
	fullname = os.path.join('data', name)
	try:
		image = pygame.image.load(fullname)
		if image.get_alpha() is None:
			image = image.convert()
		else:
			image = image.convert_alpha()
	except pygame.error, message:
		print 'Cannot load image:', fullname
		raise SystemExit, message

	return image

## actor class ############################################################

# Player states
IDLE = 0
IDLE_RANGE = (0, 0)
MOVING = 1
MOVING_RANGE = (1, 4)
FALLING = 2
FALLING_RANGE = (36, 36)
JUMPING = 3
JUMPING_RANGE = (18, 20)
KNEELING = 4
KNEELING_RANGE = (5, 5)
CLIMBING = 5
CLIMBING_RANGE = (39, 43)

PLAYER_SPRITE_SIZE = 24

class TiledSprite:
	def __init__(self, filename):
		self.bitmap = load_image(filename)
		self.bitmap.set_clip(Rect(0, 0, PLAYER_SPRITE_SIZE, PLAYER_SPRITE_SIZE))

	def set_nth(self, i):
		y = int(i / 12) * PLAYER_SPRITE_SIZE + 1
		x = int(i % 12) * PLAYER_SPRITE_SIZE + 2
		self.bitmap.set_clip(Rect(x, y, PLAYER_SPRITE_SIZE-1, PLAYER_SPRITE_SIZE))

	def get_current(self):
		return self.bitmap

class CollisionError (Exception):
	pass

class FallsError (CollisionError):
	pass

class Actor(pygame.sprite.Sprite):
	base = Sprite()
	direction = False
	moving = False
	curr_action = None

	def __init__(self, controller = None):
		pygame.sprite.Sprite.__init__(self)
		self.base.rect = Rect(0, 0, PLAYER_SPRITE_SIZE, 1)
		self.controller = controller

	def load(self, spritefile):
		self.sprite = TiledSprite(spritefile)
		self.rect = Rect(50, 100, PLAYER_SPRITE_SIZE, PLAYER_SPRITE_SIZE)

	def translate (self, x = 0, y = 0, force = False):
		self.rect.move_ip (x, y)
		
		if (x < 0) and (not self.direction):
			self.direction = not self.direction
		elif (x > 0) and (self.direction):
			self.direction = not self.direction
			
		self.moving = True
		self.base.rect.topleft = self.rect.bottomleft
		
		if not self.collision_manager.collides(self.base):
			# Has to fall
			raise FallsError

	def update(self):
		if self.moving:
			self.sprite.set_nth( MOVING_RANGE[0] )
		else:
			self.sprite.set_nth( IDLE_RANGE[0] )

		if self.direction:
			pass

        	self.moving = False

	def draw(self, buffer):
		buffer.blit(self.sprite.get_current(), self.rect, self.sprite.get_current().get_clip())
		self.new_update()

	def new_update (self):
		self.update()
		if self.curr_action and self.curr_action.is_alive:
			try:
				self.curr_action.update (self)
			except FallsError:
				self.curr_action = Fall (self.direction)
		else:
			self.curr_action = self.controller.get_action()
			if self.curr_action:
				print self.curr_action
	
	
class Action (object):
	def update (self, actor):
		pass
	
	is_alive = property (lambda self: False)

class Move (Action):
	def __init__ (self, move_left):
		self.move_left = move_left
		# moving takes 5 frames
		self.frames = 3
		
	def update (self, actor):
		amount = 3
		if self.move_left:
			amount *= -1
		actor.translate (x = amount)
		self.frames -= 1
	
	is_alive = property (lambda self: self.frames >= 0)

class Jump (Action):
	UP = 1
	LEFT = 2
	RIGHT = 3
	
	def __init__ (self, direction):
		# number of iteration it goes up
		self.frames = 20
		# number of pixels by move
		self.amount_factor = -2
		self.__is_alive = True
		self.direction = direction
		
	def update (self, actor):
		v_amount = self.amount_factor
		if self.direction == Jump.UP:
			h_amount = 0
		elif self.direction == Jump.LEFT:
			h_amount = 2
		elif self.direction == Jump.RIGHT:
			h_amount = -2
			
		self.frames -= 1
		if self.frames <= 0:
			v_amount *= -1
			
		# Use the force luke
		try:
			actor.translate (y = v_amount)
			if self.frames <=0:
				self.__is_alive = False
		except FallsError:
			pass
	
	is_alive = property (lambda self: self.__is_alive)

class Fall (Action):
	def __init__ (self, fall_left = False):
		self.vert_amount = 4
		self.horz_amount = 2
		self.fall_left = True
		self.__is_alive = True
		
	def update (self, actor):
		h_amount = self.horz_amount
		if not self.fall_left:
			h_amount *= -1
			
		try:
			actor.translate (x = h_amount, y = self.vert_amount)
			self.__is_alive = False
		except FallsError:
			pass

	is_alive = property (lambda self: self.__is_alive)

class KeyboardController (object):
	def __init__ (self, keys):
		self.commands = []
		self.keys = keys
	
	def recieve_input (self):
		if len(self.commands):
			return
		keystate = self.keys.get_pressed()
		if keystate[K_RIGHT] and keystate[K_UP]:
			self.commands.append (Jump (Jump.RIGHT))
		elif keystate[K_LEFT] and keystate[K_UP]:
			self.commands.append (Jump (Jump.LEFT))
		elif keystate[K_RIGHT]:
			self.commands.append (Move(move_left = False))
		elif keystate[K_LEFT]:
			self.commands.append (Move(move_left = True))
		elif keystate[K_DOWN]:
			pass
		elif keystate[K_UP]:
			self.commands.append (Jump (Jump.UP))

	def get_action (self):
		c = None
		if len(self.commands):
			c = self.commands[0]
			del self.commands[0]
		return c
