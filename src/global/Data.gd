extends Node

var player: Player

var regions := {
	Region.Types.FOREST: load("res://data/regions/Forest.tscn"),
	Region.Types.MOUNTAINS: load("res://data/regions/Mountains.tscn"),
}

