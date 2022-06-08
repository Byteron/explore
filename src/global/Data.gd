extends Node

var player: Player

var regions := {
	Region.Types.FOREST: load("res://data/regions/Forest.tscn"),
	Region.Types.MOUNTAINS: load("res://data/regions/Mountains.tscn"),
	Region.Types.SOUTH_SEA: load("res://data/regions/SouthSea.tscn"),
	Region.Types.FOREST_CAVE: load("res://data/regions/ForestCave.tscn"),
}

