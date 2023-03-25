function initTaskEditForm() {
	gantt.$lightboxControl.task.addForm = function () {

		var task = gantt.getTask(gantt._lightbox_id);
		if (gantt._lightbox_task) {
			task = gantt._lightbox_task;
		}

		var taskFormRows = {
			text: {
				name: "text",
				type: "input",
				label: "Task name",
				id: "text",
				labelPosition: "left",
				labelWidth: 100,
				required: true,
				value: task.text
			},
			description: {
				name: "description",
				type: "textarea",
				label: "Description",
				id: "description",
				labelPosition: "left",
				labelWidth: 100,
				value: task.description
			},
			start_date: {
				name: "start_date",
				type: "datepicker",
				label: "Start Date",
				id: "start_date",
				required: true,
				labelPosition: "left",
				labelWidth: 100,
				value: task.start_date
			},
			end_date: {
				name: "end_date",
				type: "datepicker",
				label: "End Date",
				id: "end_date",
				required: true,
				labelPosition: "left",
				labelWidth: 100,
				value: task.end_date
			},
			duration: {
				name: "duration",
				type: "input",
				inputType: "number",
				label: "Duration",
				id: "duration",
				labelPosition: "left",
				labelWidth: 100,
				required: true,
				value: task.duration
			},
			owners: {
				name: "owners",
				type: "combo",
				label: "Owners",
				id: "end_date",
				labelPosition: "left",
				labelWidth: 100,
				multiselection: true,
				value: ["1", "4"],
				data: [
					{ value: "Anna", id: "1" },
					{ value: "John", id: "2" },
					{ value: "Floe", id: "3" },
					{ value: "Bill", id: "4" },
					{ value: "Mike", id: "5" }
				],
				value: task.owners
			},
			progress: {
				name: "progress",
				type: "slider",
				id: "progress",
				label: "Progress",
				labelPosition: "left",
				labelWidth: 100,
				min: 0,
				max: 100,
				value: task.progress * 100
			}
		};


		var taskFormRowsForGrid = [
			taskFormRows["text"],
			taskFormRows["description"],
			taskFormRows["start_date"],
			taskFormRows["end_date"],
			taskFormRows["duration"],
			taskFormRows["owners"],
			taskFormRows["progress"]
		];

		if (gantt._taskForm) gantt._taskForm.destructor();
		gantt._taskForm = new dhx.Form(null, {
			css: "dhx_widget--bordered",
			rows: taskFormRowsForGrid
		});
		gantt._tabbar.getCell("task").attach(gantt._taskForm);


		gantt._taskForm.events.on("Change", function (name, new_value) {
			var task = gantt._lightbox_task;

			var updatedTask = gantt._taskForm.getValue();

			task.text = updatedTask.text;
			task.description = updatedTask.description;
			task.owners = updatedTask.owners;
			task.progress = updatedTask.progress / 100;

			var new_start_date = updatedTask.start_date;
			var new_end_date = updatedTask.end_date;
			var new_duration = updatedTask.duration;
			var old_end_date = +new Date(task.end_date);

			if (new_start_date instanceof Date) {
				// do nothing
			}
			else {
				task.start_date = gantt.date.parseDate(new_start_date, "%d/%m/%y");
			}

			if (task.duration != new_duration) {
				task.duration = new_duration;
				task.end_date = gantt.calculateEndDate({ start_date: task.start_date, duration: task.duration, task: task });
			}

			if (new_end_date instanceof Date && +old_end_date == +new_end_date) {
				// do nothing
			}
			else {
				new_end_date = gantt.date.parseDate(new_end_date, "%d/%m/%y");
				if (+old_end_date != +new_end_date) {
					task.end_date = new_end_date;
					task.duration = gantt.calculateDuration({ start_date: task.start_date, end_date: task.end_date, task: task });
				}
			}

		});
	};

}