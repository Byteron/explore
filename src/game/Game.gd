extends Node2D
class_name Game

var _region: Region

onready var player: Player = $Player
onready var camera: ZoneCamera = $ZoneCamera


func _ready() -> void:
	_spawn_region(Region.Types.FOREST)
	
	player.position = _region.spawn_point


func _spawn_region(next_region: int) -> void:
	_region = Data.regions[next_region].instance()
	_region.connect("exited", self, "_on_region_exited")
	add_child(_region)


func _on_region_exited(next_region: int, next_exit: int) -> void:
	call_deferred("_change_region", next_region, next_exit)


func _change_region(next_region: int, next_exit: int) -> void:
	
	player.set_process(false)
	Fade.fade_out()
	
	yield(Fade, "finished")
	
	_region.queue_free()
	_spawn_region(next_region)
	
	player.position = _region.exits[next_exit].position
	camera.position = Region.world2world(player.position)
	
	Fade.fade_in()
	
	yield(Fade, "finished")
	
	player.set_process(true)
