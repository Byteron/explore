extends Position2D
class_name Spawner

signal spawn_timeout(scene)

export var scene: PackedScene


func _ready() -> void:
	emit_signal("spawn_timeout", scene)
