class_name Combat


static func damage(health: Health, damage: Damage) -> void:
	health.decrease(damage.amount)
	print("dealt %d Damage to %s" % [damage.amount, health.get_parent().name])
	
	if health.is_dead():
		health.owner.queue_free()
