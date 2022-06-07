extends Camera2D
class_name ZoneCamera

export var target: NodePath
onready var _target: Node2D = get_node(target)


func _process(delta: float) -> void:
	if not _target: return
	
	position = _target.position
