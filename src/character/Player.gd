extends Character
class_name Player

export var speed := 72.0

onready var scan_area: ScanArea2D = $ScanArea2D

func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("attack"):
		print("attack clicked")
		hit()


func _process(delta: float) -> void:
	var direction = get_direction()
	
	if not direction: return
	
	scan_area.look_at(scan_area.global_position + direction)
	
	move_and_slide(direction * speed, Vector2.UP)


func hit() -> void:
	for area in scan_area.get_overlapping_areas():
		if area is HitArea2D:
			area.hit()


func get_direction() -> Vector2:
	return Input.get_vector("move_left", "move_right", "move_up", "move_down")
