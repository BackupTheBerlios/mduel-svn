#!/bin/sh

rm ASL.java
rm Token*.java
rm JJT*.java
rm Parse*.java
jjtree asl.jjt
javacc asl.jj
