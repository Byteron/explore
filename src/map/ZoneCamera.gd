extends Camera2D
class_name ZoneCamera

export var target: NodePath
onready var _target: Node2D = get_node(target)


func _ready() -> void:
	_target.connect("tree_exited", self, "_on_target_freed")


func _process(delta: float) -> void:
	if not _target: return
	position = _target.position


func _on_target_freed() -> void:
	_target = null
	
