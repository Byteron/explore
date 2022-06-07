extends Area2D
class_name Exit

signal exited(next_region, next_exit)


export(Region.Types) var next_region := 0
export var next_exit := 0


func _ready() -> void:
	connect("body_entered", self, "_on_body_entered")


func _on_body_entered(body: PhysicsBody2D) -> void:
	if not body is Character: return
	
	emit_signal("exited", next_region, next_exit)
