extends Character
class_name Enemy

onready var health: Health = $Health
onready var damage: Damage = $Damage

onready var hit_area: HitArea2D = $HitArea2D
onready var scan_area: ScanArea2D = $ScanArea2D

export var vision := 64

var origin: Vector2


func _ready() -> void:
	origin = position


func _process(delta: float) -> void:
	if not Data.player: 
		var direction = position.direction_to(origin)
		move_and_slide(direction * speed, Vector2.UP)
		return
	
	var distance = position.distance_to(Data.player.position)
	
	if distance > vision:
		var direction = position.direction_to(origin)
		move_and_slide(direction * speed, Vector2.UP)
		return
	
	var direction = position.direction_to(Data.player.position)
	
	move(direction, delta)


func _on_HitArea2D_hit(damage: Damage, origin: Vector2) -> void:
	Combat.damage(health, damage)
	knockback(origin.direction_to(position).normalized(), damage.amount)


func _on_ScanArea2D_area_entered(area: Area2D) -> void:
	if area is HitArea2D and area.owner is Player:
		area.hit(damage, position)
