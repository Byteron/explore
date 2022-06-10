extends Node2D
class_name Bush


onready var health: Health = $Health


func _on_HitArea2D_hit(damage: Damage, origin: Vector2) -> void:
	Combat.damage(health, damage)

