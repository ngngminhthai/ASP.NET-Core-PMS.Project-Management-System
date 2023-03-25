function fileDragAndDrop(){
	var overlay = null,
		uid = Date.now();

	function callListeners(file){
		for(var i in this.listeners){
			if(this.listeners[i])
				this.listeners[i](file);
		}
	}

	var showHighlight = false,
		timeout = 0;

	return {
		root: null,
		listeners: {},
		fileTypeMessage: "Only MPP and XML files are supported!",
		dndFileTypeMessage: "Please try XML or MPP project file.",
		dndHint: "Drop MPP or XML file into Gantt",
		onDrop: function(listener){
			var id = uid++;
			this.listeners[id] = listener;
			return id;
		},
		removeListener: function(id){
			delete this.listeners[id];
		},

		mode:"msp",

		isExcelMimeType: function(fileTransferItem){
			if(!fileTransferItem) return false;

			var excelTypes = {
				"application/vnd.ms-excel":true,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":true
			};

			return excelTypes[fileTransferItem.type];
		},
		isExcelFile: function(file){
			var ext = file.name.split(".").pop();
			var excelExtensions = {
				"xls": true,
				"xlsx": true
			};

			return !!(excelExtensions[ext]);
		},

		isPrimaveraP6MimeType: function(fileTransferItem){
			if(!fileTransferItem) return false;

			var types = {
				"application/xer":true
			};

			return types[fileTransferItem.type];
		},

		isPrimaveraP6File: function(file){
			var ext = file.name.split(".").pop();
			var extensions = {
				"xer": true,
				"xml": true
			};

			return !!(extensions[ext]);
		},

		isMsProjectMimeType: function(fileTransferItem){
			if(!fileTransferItem) return false;

			var msTypes = {
				"text/xml":true,
				"application/xml":true,
				"application/vnd.ms-project": true,
				"application/msproj": true,
				"application/msproject": true,
				"application/x-msproject": true,
				"application/x-ms-project": true,
				"application/x-dos_ms_project": true,
				"application/mpp": true,
				"zz-application/zz-winassoc-mpp": true
			};

			return msTypes[fileTransferItem.type];
		},

		isMsProjectFile: function(file){
			var ext = file.name.split(".").pop();
			var msProjExtensions = {
				"mpp": true,
				"xml": true
			};

			return !!(msProjExtensions[ext]);
		},

		init: function(div){

			this.root = div;

			div.addEventListener("dragover", gantt.bind(function(event){
				event.preventDefault && event.preventDefault();
				showHighlight = true;
				this.showHover(event);
			}, this), false);

			div.addEventListener("dragenter", gantt.bind(function(event){
				event.preventDefault && event.preventDefault();
				showHighlight = true;
				this.showHover(event);
			}, this), false);

			div.addEventListener("dragleave", gantt.bind(function(event){
				showHighlight = false;
				clearTimeout( timeout );
				timeout = setTimeout( gantt.bind(function(){
					if( !showHighlight ){ this.hideOverlay(); }
				}, this), 200 );
			}, this), false);

			div.addEventListener("dragend", gantt.bind(function(event){
				this.hideOverlay();
				showHighlight = false;
			}, this), false);

			div.addEventListener("drop", gantt.bind(function(event){
				event.preventDefault && event.preventDefault();
				showHighlight = false;
				this.hideOverlay();

				var files = event.dataTransfer.files;

				var file = files[0];

				var checkFileType = this.isMsProjectFile;

				if(this.mode == "excel"){
					checkFileType = this.isExcelFile;
				}else if(this.mode == "primaveraP6"){
					checkFileType = this.isPrimaveraP6File;
				}

				if(checkFileType.call(this, file)){
					callListeners.call(this, file);
				}else{
					gantt.message("The extension of <b>" +file.name+ "</b> " + this.fileTypeMessage);
				}

				return false;
			}, this), false);
		},

		hideOverlay: function(){
			if(!overlay) return;
			overlay.parentNode.removeChild(overlay);
			overlay = null;
		},

		showHover: function showFileHover(event){
			if(event.dataTransfer && event.dataTransfer.items && event.dataTransfer.items[0]){

				var checkMimeType = this.isMsProjectMimeType;

				if(this.mode == "excel"){
					checkMimeType = this.isExcelMimeType;
				}else if(this.mode == "primaveraP6"){
					checkMimeType = this.isMsProjectMimeType;
				}

				if(!checkMimeType.call(this, event.dataTransfer.items[0])){
					this.showOverlay('<div class="gantt-file-hover-content-upload-image"></div>' +
						'<div class="gantt-file-hover-content-upload-message">'+this.dndFileTypeMessage+'</div>', true);
				}else{
					this.showOverlay('<div class="gantt-file-hover-content-upload-image"></div>' +
						'<div class="gantt-file-hover-content-upload-message">'+this.dndHint+'</div>');
				}
			}
		},

		showUpload: function showFileInProgress(){
			this.showOverlay('<div class="gantt-file-upload-spinner"><div class="gantt-file-upload-spinner-inner"></div></div>' +
				'<div class="gantt-file-hover-content-upload-message">Loading&hellip;</div>');
		},

		showOverlay: function showOverlay(innerHTML, invalid){
			if(!this.root) return;
			if(overlay) return;

			overlay = document.createElement("div");
			overlay.className = "gantt-file-hover" + (invalid ? " not-supported" : "");

			overlay.innerHTML = '<div class="gantt-file-hover-inner">' +
				'<div class="gantt-file-hover-content-pending">' +
				innerHTML +
				'</div>' +
				'</div>';
			this.root.appendChild(overlay);
		}
	};
}