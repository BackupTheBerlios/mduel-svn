import pygame, os

from pygame.locals import *
from pygame.sprite import Sprite
from pygame import Rect
from display import SCREEN_SIZE_X, SCREEN_SIZE_Y

MAP_TILE_SIZE = 16
X_TILES = SCREEN_SIZE_X / MAP_TILE_SIZE
Y_TILES = SCREEN_SIZE_Y / MAP_TILE_SIZE

T_FLOOR = 1
T_STAIRS = 2
T_FLOOR2 = 3
T_EGG = 4

class Map:
	def __init__(self):
		self.map = []
		self.map_tiles = pygame.sprite.RenderPlain()

		for i in range(0, X_TILES*Y_TILES):
			self.map.append(0)

		self.floor = pygame.image.load(os.path.join("data", 
			"grass.png")).convert()
		self.stairs = pygame.image.load(os.path.join("data",
			"stairs.png")).convert()
		self.floor2 = pygame.image.load(os.path.join("data",
			"grasswithstairs.png")).convert()

	def load_from_file (self, file):
		self.map = []
		y = 0
		for line in file.readlines():
			line = line.strip()
			x = 0
			for c in line:
				self.__append_tile (int(c), x, y)
				x += MAP_TILE_SIZE
			y += MAP_TILE_SIZE
	
	def __append_tile (self, tile_type, x, y):
		if tile_type == T_FLOOR:
			spr = Sprite()
			spr.rect = Rect (x, y, MAP_TILE_SIZE, 1)
			spr.image = self.floor
			self.map_tiles.add(spr)

	def collides(self, sprite):
		return len (self.collisions(sprite)) > 0

	def collisions(self, sprite):
		return pygame.sprite.spritecollide(sprite, self.map_tiles, False)

	def draw(self, buffer):
		self.map_tiles.draw (buffer)
