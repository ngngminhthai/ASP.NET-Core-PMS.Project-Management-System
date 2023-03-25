module.exports = {
	data: function(task) {
		task.open = task.open * 1;
		task.progress = task.progress * 1;
		task.duration = task.duration * 1;
		task.id = task.id * 1;
		task.parent = task.parent * 1;
		return task;
	},
	links: function(link){
		link.id = link.id * 1;
		link.source = link.source * 1;
		link.source = link.source * 1;
		return link;
	}
};