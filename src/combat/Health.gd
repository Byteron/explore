extends Node
class_name Health

export var maximum := 100
var current: int


func _ready() -> void:
	current = maximum


func increase(amount: int) -> void:
	current = clamp(current + amount, 0, maximum)


func decrease(amount: int) -> void:
	current = clamp(current - amount, 0, maximum)


func restore() -> void:
	current = maximum


func is_dead() -> bool:
	return current == 0
