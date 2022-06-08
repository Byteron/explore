extends KinematicBody2D
class_name Enemy

onready var health: Health = $Health

export var vision := 64
export var speed := 35

var origin: Vector2


func _ready() -> void:
	origin = position


func _process(delta: float) -> void:
	if not Data.player: return
	
	var distance = position.distance_to(Data.player.position)
	
	if distance > vision or distance < 24:
		var direction = position.direction_to(origin)
		move_and_slide(direction * speed, Vector2.UP)
		return
	
	var direction = position.direction_to(Data.player.position)
	move_and_slide(direction * speed, Vector2.UP)


func _on_HitArea2D_hit(damage: Damage) -> void:
	Combat.damage(health, damage)
