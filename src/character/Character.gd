extends KinematicBody2D
class_name Character

export var can_swim := false

func _ready() -> void:
	set_collision_mask_bit(2, !can_swim)
