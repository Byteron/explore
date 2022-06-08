extends Area2D
class_name HitArea2D

signal hit(damage)


func hit(damage: Damage) -> void:
	emit_signal("hit", damage)
