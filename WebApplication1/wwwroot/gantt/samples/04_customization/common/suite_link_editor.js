function initLinkEditForm() {
	gantt.$lightboxControl.links.addForm = function () {

		this.deleteLink = function (id) {
			gantt._removed_links.push(id);
			gantt._linkEditor.data.remove(id);
			this.updateLightboxLinkData();
		};

		this.removeAllLinks = function () {
			var links = gantt._linkEditor.data._order;
			for (var i = 0; i < links.length; i++) {
				gantt._removed_links.push(links[i].id);
				gantt._linkEditor.data.remove(links[i].id);
			}
			this.updateLightboxLinkData();
		};

		this.addNewLink = function () {
			gantt._linkEditor.data.add([{
				id: +new Date(),
				target: "",
				source: gantt._lightbox_id,
				type: 0,
				task: "",
				link_type: "Finish to Start",
				direction: "Predecessor",
				lag: 0
			}]);
			this.updateLightboxLinkData();
		}

		this.updateLightboxLinkData = function () {
			gantt._lightbox_links = gantt._linkEditor.data._order;
		}



		var linkColumns = [
			{ width: 50, id: "add_link", header: [{ text: "<input class='dhx_button dhx_button--size_small' type=button value='+' title='Add a new link' data-onclick='addNewLink'>" }], sortable: false, },
			{
				minWidth: 150, id: "task", header: [{ text: "task" }], editorType: "select", options: [], template: function (text, row, col) {
					return col.optionLabels[text];
				}
			},
			{ width: 120, id: "direction", header: [{ text: "direction" }], editorType: "select", options: ["Predecessor", "Successor",] },
			{ width: 120, id: "link_type", header: [{ text: "Type" }], editorType: "select", options: ["Finish to Start", "Start to Start", "Finish to Finish", "Start to Finish"] },
			{ width: 80, id: "lag", header: [{ text: "Lag" }], editorType: "input", type: "number" },
			{
				width: 50, id: "remove_link", header: [{ text: "<input type=button class='dhx_button dhx_button--size_small' value='✖' title='Delete all links' data-onclick='removeAllLinks'>" }], sortable: false, htmlEnable: true, template: function (text, row, col) {
					return "<input class='dhx_button dhx_button--size_small' type=button value='✖' title='Delete this link' data-onclick='deleteLink' data-onclick_argument='" + row.id + "'>";
				}
			},
		]

		if (gantt._linkEditor) {
			gantt._linkEditor.destructor();
		}

		if (gantt._lightbox_links == "load") {
			gantt._lightbox_links = [];
			var task = gantt._lightbox_task;
			var predecessors = task.$target;
			var successors = task.$source;

			predecessors.forEach(function (linkId) {
				var link = gantt.getLink(linkId);
				if (!gantt.isTaskExists(link.source)) return;

				link.task = link.source;
				link.link_type = linkColumns[3].options[link.type];
				link.direction = "Predecessor";
				link.lag = link.lag || 0;

				gantt._lightbox_links.push(link);
			})


			successors.forEach(function (linkId) {
				var link = gantt.getLink(linkId);
				if (!gantt.isTaskExists(link.target)) return;

				link.task = link.target;
				link.link_type = linkColumns[3].options[link.type];
				link.direction = "Successor";
				link.lag = link.lag || 0;

				gantt._lightbox_links.push(link);
			});
		}


		linkColumns[1].options = [];
		linkColumns[1].optionLabels = {};
		var tasks = gantt.getTaskByTime()
		tasks.forEach(function (task) {
			if (task.id != gantt.getState().lightbox) {
				linkColumns[1].options.push(task.id);
				linkColumns[1].optionLabels[task.id] = task.text;
			}
		})

		gantt._linkEditor = new dhx.Grid(null, {
			columns: linkColumns,
			autoHeight: true,
			autoWidth: true,
			editable: true,
			data: gantt._lightbox_links
		});

		gantt._linkEditor.events.on("CellClick", function (row, column, e) {
			gantt._linkEditor.editCell(row.id, column.id);
		});

		gantt._linkEditor.events.on("AfterEditStart", function (row, col, editorType) {
			if (col.id == "lag") {
				dhx.awaitRedraw().then(function () {
					var element = document.querySelector(".dhx_cell-editor");
					element.type = "number";
				});
			}
			if (col.id == "task") {
				setTimeout(function () {
					var selectEl = document.querySelector(".dhx_cell-editor__select");
					var selectedValue = selectEl.value;
					var children = selectEl.childNodes;
					for (var i = 0; i < children.length; i++) {
						var child = children[i];
						child.outerHTML = "<option value=" + child.innerHTML + ">" + linkColumns[1].optionLabels[child.innerHTML] + "</option>";
					}
					selectEl.value = selectedValue;
				}, 50);
			}

		});

		gantt._linkEditor.events.on("BeforeEditEnd", function (value, row, column) {
			var id = row.id
			for (var i = 0; i < gantt._lightbox_links.length; i++) {
				var link = gantt._lightbox_links[i];
				if (link.id != id) {
					continue;
				}

				if (column.id == "task") {
					var selectedTaskId = value;
					if (link.direction == "Predecessor") {
						link.source = selectedTaskId;
						link.target = gantt._lightbox_id;
					}
					else {
						link.source = gantt._lightbox_id;
						link.target = selectedTaskId;
					}

				}
				if (column.id == "direction") {
					var tmpProperty = link.source;
					link.source = link.target;
					link.target = tmpProperty;
				}
				if (column.id == "link_type") {
					link.type = column.options.indexOf(value);
				}
			}
		});
		gantt._tabbar.getCell("links").attach(gantt._linkEditor);
	};

}