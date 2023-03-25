(function(){
	function defineMultiselect(gantt) {
		gantt.form_blocks["multiselect"] = {
			render: function (sns) {
				var height = (sns.height || "23") + "px";
				var html = "<div class='gantt_cal_ltext gantt_cal_chosen gantt_cal_multiselect' style='height:" + height + ";'><select data-placeholder='...' class='chosen-select' multiple>";
				if (sns.options) {
					for (var i = 0; i < sns.options.length; i++) {
						if(sns.unassigned_value !== undefined && sns.options[i].key == sns.unassigned_value){
							continue;
						}
						html += "<option value='" + sns.options[i].key + "'>" + sns.options[i].label + "</option>";
					}
				}
				html += "</select></div>";
				return html;
			},
	
			set_value: function (node, value, ev, sns) {
				node.style.overflow = "visible";
				node.parentNode.style.overflow = "visible";
				node.style.display = "inline-block";
				var select = $(node.firstChild);
	
				if (value) {
					value = (value + "").split(",");
					select.val(value);
				}
				else {
					select.val([]);
				}
	
				select.chosen();
				if(sns.onchange){
					select.change(function(){
						sns.onchange.call(this);
					})
				}
				select.trigger('chosen:updated');
				select.trigger("change");
			},
	
			get_value: function (node, ev) {
				var value = $(node.firstChild).val();
				return value;
			},
	
			focus: function (node) {
				$(node.firstChild).focus();
			}
		};

	}

	if(window.Gantt){
		window.Gantt.plugin(defineMultiselect);
	}else{
		defineMultiselect(window.gantt);
	}

})();