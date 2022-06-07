extends Node2D
class_name Bush


func _on_HitArea2D_hit() -> void:
	queue_free()
