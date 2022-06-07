extends Camera2D
class_name ZoneCamera

export var target: NodePath
onready var _target: Node2D = get_node(target)

var _zone := Vector2(0, 0)
var _direction := Vector2(0, 0)
var _tween: SceneTreeTween

func _process(delta: float) -> void:
	if not _target: return
	
	var zone = Region.world2zone(_target.position)
	
	if zone == _zone: return
	
	_direction = zone -_zone
	_zone = zone
	
	move(Region.zone2world(zone))


func move(new_position: Vector2) -> void:
	if _tween and _tween.is_running():
		_tween.stop()
	
	_tween = get_tree().create_tween()
	
	var speed = (_direction * Region.ZONE_SIZE).length() * 0.02
	
	_tween.tween_property(self, "position", new_position, speed).set_ease(Tween.EASE_IN_OUT).set_trans(Tween.TRANS_SINE)
	_tween.play()
