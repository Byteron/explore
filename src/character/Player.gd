extends Character
class_name Player

var velocity : Vector2

export var speed := 72.0
export var acceleration := 1000.0
export var friction := 0.2

onready var scan_area: ScanArea2D = $ScanArea2D


func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("attack"):
		print("attack clicked")
		hit()


func _process(delta: float) -> void:
	var direction = get_direction()
	
	if not direction.x: velocity.x = lerp(velocity.x, 0.0, friction)
	if not direction.y: velocity.y = lerp(velocity.y, 0.0, friction)
	
	if not direction: return
	
	
	scan_area.look_at(scan_area.global_position + direction)
	
	velocity += direction * acceleration * delta
	velocity = velocity.clamped(speed)
	
	move_and_slide(velocity, Vector2.UP)


func hit() -> void:
	for area in scan_area.get_overlapping_areas():
		if area is HitArea2D:
			area.hit()


func get_direction() -> Vector2:
	return Input.get_vector("move_left", "move_right", "move_up", "move_down")
