extends KinematicBody2D
class_name Character

var velocity: Vector2
var motion: Vector2
var force: Vector2

var stagger: float


export var can_swim := false

export var speed := 72.0
export var acceleration := 1000.0
export var friction := 0.2


onready var sprite: Sprite = $Sprite


func _ready() -> void:
	set_collision_mask_bit(2, !can_swim)


func _process(delta: float) -> void:
	stagger = clamp(stagger - delta, 0, stagger)
	
	force.x = lerp(force.x, 0, friction)
	force.y = lerp(force.y, 0, friction)
	
	if not motion:
		motion.x = lerp(motion.x, 0, friction)
		motion.y = lerp(motion.y, 0, friction)
	
	velocity = force if stagger else motion + force
	
	motion = Vector2.ZERO
	
	if velocity: move_and_slide(velocity, Vector2.UP)


func move(direction: Vector2, delta: float) -> void:
	motion = direction * speed


func knockback(direction: Vector2, force: float) -> void:
	self.force += direction * (force * 5 + 500)
	stagger = 0.2
