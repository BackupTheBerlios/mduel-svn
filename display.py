
from pygame.locals import *
import pygame.display

SCREEN_SIZE = (320, 200)
SCREEN_SIZE_X = SCREEN_SIZE[0]
SCREEN_SIZE_Y = SCREEN_SIZE[1]

class Display:
	def __init__(self, fullscreen):
		pygame.display.init()

		self.set_mode(SCREEN_SIZE, fullscreen)
		self.backbuffer = pygame.Surface(self.screen.get_size())

	def set_mode(self, res, fullscreen):
		self.res = res
		self.fullscreen = fullscreen

		if fullscreen:
			self.screen = pygame.display.set_mode(res,
				HWSURFACE | FULLSCREEN)
		else:
			self.screen = pygame.display.set_mode(res, HWSURFACE)

	def get_surface(self):
		return self.screen
	
	def get_backbuffer(self):
		return self.backbuffer

	def set_title(self, title):
		if not self.fullscreen:
			pygame.display.set_caption(title)

	def hide_mouse(self):
		pygame.mouse.set_visible(0)
	
	def show_mouse(self):
		pygame.mouse.set_visible(1)
