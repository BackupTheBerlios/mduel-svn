#!/bin/env python2

import pygame, os
from pygame.locals import *

from display import Display
from actor import Actor, KeyboardController
from map import Map

## game class #############################################################

#def get_possible_moves(map, actor):


class ActorCollisionManager(object):
	def __init__ (self, map):
		self.map = map
	
	def collides (self, actor):
		return self.map.collides(actor)
	
	def collisions (self, actor):
		return self.map.collisions(actor)
		
class Game:
	def __init__(self):
		pygame.init()
		d = Display(False)
		self.backbuffer = d.get_backbuffer()
		self.screen = d.get_surface()
		d.set_title("Marshmallow Duels - The Final Showdown")
		self.running = True

	def Run(self):
		a = Actor()
		a.controller = KeyboardController(pygame.key)
		a.load("MD15.DAT")

		map = Map()
		#map.load()
		f = open("data/mapa.txt");
		map.load_from_file(f);
		f.close()
		
		a.collision_manager = ActorCollisionManager(map)

		while self.running:
			pygame.time.delay(10)

			for event in pygame.event.get():
				if (event.type == QUIT) or \
				   (event.type == KEYDOWN and \
				   event.key == K_ESCAPE):
				   self.running = False
			self.backbuffer.fill((0,0,0))
			map.draw(self.backbuffer)
			a.controller.recieve_input()
			a.draw(self.backbuffer)

			self.screen.blit(self.backbuffer, (0,0))
			pygame.display.update()
			pygame.display.flip()

## the main crap ##########################################################

if __name__ == '__main__':
	g = Game()
	g.Run()

## the end ################################################################
