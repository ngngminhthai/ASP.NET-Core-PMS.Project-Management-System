(function(){
	function setupResourceFilter(gantt) {
		gantt.attachEvent("onGanttReady", function(){

			var resourcesStore = gantt.getDatastore(gantt.config.resource_store);

			if(!resourcesStore){
				return;
			}

			var filterValue;
			function selectResource() {
				var node = this;
				filterValue = node.value;
				gantt.render();
			}

			resourcesStore.attachEvent("onFilterItem", function(id, item) {
				if(!filterValue){
					return true;
				}
				if(id == filterValue){
					if (item.parent) {
						var parentItem = resourcesStore.getItem(item.parent);
						if (!parentItem.$open) {
							resourcesStore.open(item.parent);
						}
					}
					return true;
				}else if(resourcesStore.isChildOf(id, filterValue)){
					return true;
				}else{
					return false;
				}
			});

			function updateSelect(options) {
				if (!gantt.$container) {
					filterValue = null;
					return;
				}
				var select = gantt.$container.querySelector(".resource-select");
				var html = [];
				html.push("<option value=''>All</option>");
				options.forEach(function(option) {
					html.push("<option value='" + option.id + "'>" + option.text + "</option>");
				});
				select.innerHTML = html.join("");
				select.value = filterValue || "";
			}

			resourcesStore.attachEvent("onParse", function() {
				updateSelect(resourcesStore.getItems());
			});

			var select = gantt.$container.querySelector(".resource-select");
			if(select){
				select.onchange = selectResource;
			}
		});
	}

	if(window.Gantt){
		window.Gantt.plugin(setupResourceFilter);
	}else{
		setupResourceFilter(window.gantt);
	}

})();
