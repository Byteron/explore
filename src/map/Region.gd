extends Node2D
class_name Region

enum Types {
	FOREST,
	MOUNTAINS,
}

const TILE_SIZE := Vector2(16, 16)
const ZONE_SIZE := Vector2(21, 13)

signal exited(next_region, next_exit)

onready var exits := $Exits.get_children()

onready var ground = $Ground
onready var walls = $Walls

onready var spawn_point: Vector2 = $Spawn.position


func _ready() -> void:
	for exit in exits:
		exit.connect("exited", self, "_on_exit_exited")


func _on_exit_exited(next_region: int, next_exit: int) -> void:
	emit_signal("exited", next_region, next_exit)


static func world2zone(position: Vector2) -> Vector2:
	return (position / ZONE_SIZE / TILE_SIZE).floor()


static func zone2world(zone: Vector2) -> Vector2:
	return zone * TILE_SIZE * ZONE_SIZE

	
static func world2world(position: Vector2) -> Vector2:
	return zone2world(world2zone(position))
