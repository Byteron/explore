extends CanvasLayer

signal finished()


var _tween: SceneTreeTween

onready var rect: ColorRect = $ColorRect


func fade_in() -> void:
	_stop()
	
	_tween = get_tree().create_tween()
	_tween.tween_property(rect, "color:a", 0.0, 0.35)
	_tween.tween_callback(self, "emit_signal", ["finished"])
	_tween.play()


func fade_out() -> void:
	_stop()
	
	_tween = get_tree().create_tween()
	_tween.tween_property(rect, "color:a", 1.0, 0.35)
	_tween.tween_callback(self, "emit_signal", ["finished"])
	_tween.play()


func _stop() -> void:
	if _tween and _tween.is_running():
		_tween.stop()
