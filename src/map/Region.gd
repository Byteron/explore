extends Node2D
class_name Region

enum Types {
	FOREST,
	MOUNTAINS,
}

const TILE_SIZE := Vector2(16, 16)

signal exited(next_region, next_exit)

onready var exits := $Exits.get_children()

onready var ground: TileMap = $Ground
onready var walls: TileMap = $Walls

onready var spawn_point: Vector2 = $Spawn.position


func _ready() -> void:
	for exit in exits:
		exit.connect("exited", self, "_on_exit_exited")


func _on_exit_exited(next_region: int, next_exit: int) -> void:
	emit_signal("exited", next_region, next_exit)
