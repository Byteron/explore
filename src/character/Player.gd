extends Character
class_name Player


var invulnerable: float


onready var health: Health = $Health
onready var damage: Damage = $Damage

onready var scan_area: ScanArea2D = $ScanArea2D
onready var hit_area: HitArea2D = $HitArea2D

func _ready() -> void:
	Data.player = self


func _exit_tree() -> void:
	Data.player = null


func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("attack"):
		print("attack clicked")
		hit()


func _process(delta: float) -> void:
	invulnerable = clamp(invulnerable - delta, 0, invulnerable)
	
	if invulnerable:
		hit_area.monitorable = false
		sprite.modulate.a = 0.5
	else:
		hit_area.monitorable = true
		sprite.modulate.a = 1
	
	var direction = get_direction()
	
	if not direction: return
	
	scan_area.look_at(scan_area.global_position + direction)
	
	move(direction, delta)


func hit() -> void:
	for area in scan_area.get_overlapping_areas():
		if area is HitArea2D and not area == hit_area:
			area.hit(damage, position)


func get_direction() -> Vector2:
	return Input.get_vector("move_left", "move_right", "move_up", "move_down")


func _on_HitArea2D_hit(damage, origin) -> void:
	Combat.damage(health, damage)
	invulnerable = 0.75
	knockback(origin.direction_to(position).normalized(), damage.amount)
