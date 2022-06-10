extends Area2D
class_name HitArea2D

signal hit(damage, origin)


func hit(damage: Damage, origin: Vector2) -> void:
	emit_signal("hit", damage, origin)
