extends Area2D
class_name HitArea2D

signal hit()


func hit() -> void:
	emit_signal("hit")
