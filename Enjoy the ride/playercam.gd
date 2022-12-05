extends Camera2D

onready var topleft = $limit/topleft
onready var bottomright = $limit/bottomright

func _ready():
	limit_top = topleft.position.y
	limit_left = topleft.position.x
	limit_right = bottomright.position.x
	limit_bottom = bottomright.position.y
