
function showSourceCode(folder, file){
	var source_code = document.querySelector("#source_code");
	source_code.innerHTML = '<textarea id="source_code_text" disabled="true">' + atob(sampleSource[folder][file]) + "</textarea>";
}

function addCodeHighlight(){
	var codeMirror = document.querySelector(".CodeMirror");
	if (codeMirror) codeMirror.parentNode.removeChild(codeMirror);

	var editor = CodeMirror.fromTextArea(document.querySelector("textarea"), {
		mode: "htmlmixed",
		styleActiveLine: true,
		lineNumbers: true,
		lineWrapping: true,
		matchBrackets: true,
		extraKeys: { "Ctrl-Space": "autocomplete" }
	});
}

function addApiReference(folder, file){
	//clear when switching to another sample:
	var propertiesSection = document.querySelector(".properties");
	var templatesSection = document.querySelector(".templates");
	var methodsSection = document.querySelector(".methods");
	var eventsSection = document.querySelector(".events");
	var otherSection = document.querySelector(".other");
	var suggestionsSection = document.querySelector(".suggestions");
	var filesSection = document.querySelector(".files");

	propertiesSection.innerHTML = '';
	templatesSection.innerHTML = '';
	methodsSection.innerHTML = '';
	eventsSection.innerHTML = '';
	otherSection.innerHTML = '';
	suggestionsSection.innerHTML = '';
	filesSection.innerHTML = '';

	function appendAdditionalFiles(line, type){
		if (line.indexOf("dhtmlxgantt") > -1) {
			return;
		}
		var linkProperty = "src";
		if (type == "css"){
			linkProperty = "href";
		}
		type = "." + type;

		var srcIndex = line.indexOf(linkProperty);
		if (srcIndex > -1){
			var leftUrlPart = line.slice(srcIndex + type.length + 2);
			var rightUrlPart = null;
			var filename = null;
			if (leftUrlPart.indexOf("googleapis") > -1) {
				rightUrlPart = leftUrlPart.split("\"")[0];
				filename = "Google API file";
			}
			else {
				rightUrlPart = leftUrlPart.split(type)[0] + type;
				var fileNameIndex = rightUrlPart.lastIndexOf("/");
				filename = rightUrlPart.slice(fileNameIndex + 1);
			}

			var url = null;
			if (rightUrlPart.indexOf("http") > -1){
				url = rightUrlPart;
			}
			else {
				var currentFolder = location.href.substring(0, location.href.lastIndexOf('/'));
				url = currentFolder + "/" + folder + "/" + rightUrlPart;
			}

			var fileElement = document.createElement("div");
			fileElement.innerHTML = "<a href=" + url + " target='_blank'>" + filename + "</a>";
			filesSection.appendChild(fileElement);
		}
	}

	var lines = atob(sampleSource[folder][file]).split("\n");
	for (var i = 0; i < lines.length; i++) {
		var line = lines[i];

		if (line.indexOf("<title>") > -1){
			var sampleName = line.split("<title>").join('').split("</title>").join('').trim();
			var suggestionsElement = document.createElement("div");
			suggestionsElement.innerHTML = "<a href = \"https://www.google.com/search?q=" + sampleName + " site:docs.dhtmlx.com/gantt\" target='_blank'>" + sampleName + "</a>";
			suggestionsSection.appendChild(suggestionsElement);
		}
		if (line.indexOf("<script") > -1){
			appendAdditionalFiles(line, "js");
		}
		if (line.indexOf("<link") > -1){
			appendAdditionalFiles(line, "css");
		}

		var indexStart = line.indexOf("gantt.");
		if (indexStart > -1){
			var leftCut = line.slice(indexStart);
			var middleIndex = leftCut.indexOf(".");
			var middleCut = leftCut.slice(middleIndex+1);
			// second occurence
			if (middleCut.indexOf("gantt.") > -1){
				lines.push(middleCut);
				middleCut = middleCut.split("gantt.")[0];
			}

			if (middleCut.indexOf("config.") > -1){
				var configValue = parseLine(middleCut, "config.");
				if (!configValue || isCustomProperty('gantt.config.' + configValue)){
					continue;
				}

				var configElement = document.createElement("div");
				configElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_" + configValue + "_config.html target='_blank'>" + configValue + "</a>";

				if (checkDuplicateNodes(propertiesSection, configElement.innerHTML)) {
					propertiesSection.appendChild(configElement);
				}

			}
			else if (middleCut.indexOf("templates.") > -1){
				var templateValue = parseLine(middleCut, "templates.");
				if (!templateValue || isCustomProperty('gantt.templates.' + templateValue)){
					continue;
				}

				var templateElement = document.createElement("div");
				templateElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_" + templateValue + "_template.html target='_blank'>" + templateValue + "</a>";

				if (checkDuplicateNodes(templatesSection, templateElement.innerHTML)) {
					templatesSection.appendChild(templateElement);
				}
			}
			else if (middleCut.indexOf("ext.") > -1){
				var extValue = parseLine(middleCut, "ext.");
				if (!extValue){
					continue;
				}
				var postfix = '_ext';
				if (extValue == "zoom") {
					postfix = '';
				}
				if (extValue == "inlineEditors") {
					extValue = "inline_editors";
				}

				var extElement = document.createElement("div");
				extElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/desktop__" + extValue + postfix + ".html target='_blank'>" + extValue + "</a>";

				if (checkDuplicateNodes(otherSection, extElement.innerHTML)) {
					otherSection.appendChild(extElement);
				}
			}
			else if (middleCut.indexOf("attachEvent") > -1){
				var eventValue = parseLine(middleCut, "attachEvent(", 13);
				if (!eventValue || isCustomProperty('gantt.events.' + eventValue)){
					continue;
				}
				if (middleCut.indexOf("$resourcesStore") > -1){
					continue;
				}

				var eventElement = document.createElement("div");
				eventElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_" + eventValue.toLowerCase() + "_event.html target='_blank'>" + eventValue + "</a>";

				if (checkDuplicateNodes(eventsSection, eventElement.innerHTML)) {
					eventsSection.appendChild(eventElement);
				}
			}
			else if ((middleCut.indexOf("date.") > -1) || (middleCut.indexOf("date[") > -1)){
				var indexOfDate = middleCut.indexOf("date.");
				if (indexOfDate < 0){
					indexOfDate = middleCut.indexOf("date[");
				}

				var tmpLeft = middleCut.slice(indexOfDate + 5);
				var tmpRight = tmpLeft.split("=")[0];
				var dateProcessValue = tmpRight.replace(/[\W]+/g, '.').split(".")[0].match(/\w/g).join("");

				var dateProcessElement = document.createElement("div");
				dateProcessElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_date_other.html target='_blank'>gantt.date." + dateProcessValue + "</a>";

				if (checkDuplicateNodes(otherSection, dateProcessElement.innerHTML)) {
					otherSection.appendChild(dateProcessElement);
				}
			}
			else if ((middleCut.indexOf("locale.") > -1) || (middleCut.indexOf("i18n.") > -1)){
				var localizationElement = document.createElement("div");
				localizationElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/desktop__localization.html target='_blank'>Localization</a>";

				if (checkDuplicateNodes(otherSection, localizationElement.innerHTML)) {
					otherSection.appendChild(localizationElement);
				}

				var localeElement = document.createElement("div");
				localeElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_locale_other.html target='_blank'>gantt.locale</a>";

				if (checkDuplicateNodes(otherSection, localeElement.innerHTML)) {
					otherSection.appendChild(localeElement);
				}
			}

			else if ((middleCut.indexOf("utils") > -1)){
				var utilsElement = document.createElement("div");
				utilsElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_utils_other.html target='_blank'>utils</a>";

				if (checkDuplicateNodes(otherSection, utilsElement.innerHTML)) {
					otherSection.appendChild(utilsElement);
				}
			}

			else if (middleCut.indexOf("(") > -1){
				var tmpRight = middleCut.split("(")[0];
				var tmpValue = tmpRight.replace(/[\W]+/g, '.').split(".")[0].match(/\w/g);
				var methodValue = null;
				if (tmpValue && tmpValue[0]) {
					methodValue = tmpValue.join("");
				}
				else {
					continue;
				}

				if (isCustomProperty("gantt." + methodValue)){
					continue;
				}

				var methodElement = document.createElement("div");
				methodElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/api__gantt_" + methodValue.toLowerCase() + ".html target='_blank'>" + methodValue + "</a>";

				if (methodValue == "ignore_time"){
					methodElement.innerHTML = "<a href = https://docs.dhtmlx.com/gantt/desktop__custom_scale.html target='_blank'>" + methodValue + "</a>";
				}

				if (checkDuplicateNodes(methodsSection, methodElement.innerHTML)) {
					methodsSection.appendChild(methodElement);
				}
			}
		}
	}



}

function parseLine(middleCut, value, length){
	length = length || value.length;

	var tmpLeft = middleCut.slice(middleCut.indexOf(value)+length);
	var tmpRight = tmpLeft.split("=")[0];
	var tmpValue = tmpRight.replace(/[\W]+/g, '.').split(".")[0].match(/\w/g);

	if (tmpValue && tmpValue[0]) {
		return tmpValue.join("");
	}

	return false;

}

function checkDuplicateNodes(el, content){
	var children = el.childNodes;
	for (var i = 0; i < children.length; i++) {
		if (children[i].innerHTML == content) {
			return false;
		}
	}
	return true;
}

// Do not generate links for custom properties
function isCustomProperty(value){
	var customProperties = [
		'gantt.config.add_column',
		'gantt.config.font_width_ratio',
		'gantt.config.show_drag_vertical',
		'gantt.config.show_drag_dates',
		'gantt.config.drag_label_width',
		'gantt.templates.drag_date',
		'gantt.config.drag_date',
		'gantt.config.show_slack',
		'gantt.config.scroll_position',
		'gantt.templates.drag_date',
		'gantt.$container',
		'gantt.performAction'
	];
	if (customProperties.indexOf(value) > -1) {
		return true;
	}
	else {
		return false;
	}
}

window.addEventListener('click', function(e){
	if (e.target.classList.contains("link")) {
		var firstLaunch = false;
		var previousHighlights = document.querySelectorAll("[data-highlighted='true']");
		if (previousHighlights[0]){
			for (var i = 0; i < previousHighlights.length; i++) {
				previousHighlights[i].dataset.highlighted = false;
				previousHighlights[i].classList.remove("active");
			}
		}
		else {
			firstLaunch = true;

			document.querySelector(".demo").childNodes[1].addEventListener('load', function(e){
				document.querySelector(".demo").classList.remove("loading");
			});
		}

		var el = e.target;
		var folder = el.dataset.folder;
		var file = el.id;
		var demoFrame = document.querySelector(".demo").childNodes[1];

		el.classList.add("active");
		el.dataset.highlighted = true;

		el.parentNode.classList.add("active");
		el.parentNode.dataset.highlighted = true;

		showSourceCode(folder, file);

		if (isFolder()) {
			document.querySelector("#current_sample").href = "./" + file;
		}
		else {
			document.querySelector("#current_sample").href = folder + "/" + file;
		}
		var currentUrl = window.location.protocol + "//" + window.location.host + window.location.pathname,
		currentSample = document.querySelector("#current_sample"),
		sample = currentSample.attributes.href.value,
		filter = document.querySelector(".search-field").value,
		link = currentUrl + "?sample='" + sample + "'&filter='" + filter + "'";
		window.history.replaceState("", "Gantt samples", link);

		demoFrame.src = "";
		try{
			addApiReference(folder, file);
		} catch (e){}

		setTimeout(function(){
			addCover();
		});

		try{
			toggle_demo("demo");
		} catch (e){}

		setTimeout(function() {

			if (el.dataset.level) {
				demoFrame.src = "./" + file;
			}
			else {
				demoFrame.src = "./" + folder + "/" + file;
			}
			addCover();
		},200);

		if(!firstLaunch){
			document.getElementById("nav-dropdown-list").classList.remove("opened");
			document.getElementById("nav-dropdown-chosen").innerText = "Demo";
		}
	}

	var share_click =
		e.target.classList.contains('share_link') ||
		e.target.classList.contains('share_button') ||
		e.target.classList.contains('share_dialog') ||
		e.target.parentNode.classList.contains('share_dialog') ||
		e.target.classList.contains('share');

	if (!share_click) {
		removeShareDialog();
	}
});

function addCover() {
	var parent = document.querySelector(".demo");

	parent.classList.add("loading")
}


function toggle_demo(type) {
	var views = {};

	views.demo = document.querySelector("#x6");
	views.code = document.querySelector("#source_code");
	views.api = document.querySelector("#api_reference");

	for(var el in views){
		views[el].style.display = "none";
	}
	views[type].style.display = "block";

	var tabs = {};

	tabs.demo = document.getElementsByClassName("show_demo");
	tabs.code = document.getElementsByClassName("show_code");
	tabs.api = document.getElementsByClassName("show_api");

	for(var el in tabs){
		for(var i = 0; i < tabs[el].length; i++){
			tabs[el][i].classList.remove("active");
		}
	}

	for(var i = 0; i < tabs[type].length; i++){
		tabs[type][i].classList.add("active");
	}

	if (type == "code"){
		addCodeHighlight();
	}
}

function is_mobile() {
	var is_mobile_device = navigator.userAgent.match(/Android|webOS|iPhone|iPad|iPod|BlackBerry|Windows Phone/i) ? true : false;

	return is_mobile_device;
}

function toggle_list(){
	var pageAsideElem = document.getElementById("page-aside");

	if(pageAsideElem.classList.contains("aside-state") && is_mobile()){
		bodyScrollLock.enableBodyScroll(pageAsideElem);
	} else{
		bodyScrollLock.disableBodyScroll(pageAsideElem);
	}

	pageAsideElem.classList.toggle("aside-state");
}

function toggle_mobile_menu(e) {
	if (e.target.classList.contains("page-aside")) {
		toggle_list();
	}
}


function filterSamples(value) {
	var links = document.querySelector(".links");
	var folders = links.querySelectorAll("input");

	for (var i = 0; i < folders.length; i++) {
		folders[i].checked = !!value;
	}

	var files = links.querySelectorAll(".link");
	var results = false;
	var showWithChildren = {};

	for (var i = 0; i < files.length; i++) {
		var file = files[i];
		if (value && file.innerHTML.toLowerCase().indexOf(value.toLowerCase()) < 0) {
			file.style.display = "none";
		}
		else {
			file.style.display = "";
			results = true;
			var relatedFolder = file.parentNode.parentNode.querySelector("label");
			if (relatedFolder){
				showWithChildren[relatedFolder.innerHTML] = true;
			}
		}
	}

	var labels = links.querySelectorAll("label");
	for (var i = 0; i < labels.length; i++) {
		if (value && labels[i].innerHTML.toLowerCase().indexOf(value.toLowerCase()) < 0) {
			labels[i].classList.add("hidden");
		}
		else {
			labels[i].classList.remove("hidden");
			var childSamples = labels[i].parentNode.querySelectorAll(".link");
			for (var j = 0; j < childSamples.length; j++) {
				childSamples[j].style.display = "";
			}
			results = true;
		}
		if (showWithChildren[labels[i].innerHTML]) {
			labels[i].classList.remove("hidden");
			results = true;
		}
	}

	var noResults = document.querySelector(".no_results");

	if (results) {
		noResults.classList.remove("visible");
	}
	else {
		noResults.classList.add("visible");
	}
}

function isFolder() {
	var folderName = false;
	var path = window.location.pathname;
	var sampleFolders = [
		"01_initialization",
		"02_extensions",
		"03_scales",
		"04_customization",
		"05_lightbox",
		"06_skins",
		"07_grid",
		"08_api",
		"09_worktime",
		"10_layout",
		"11_resources",
		"20_multiple"
	];
	sampleFolders.forEach(function (folder) {
		if (path.indexOf(folder) > -1) {
			folderName = folder
		}
	})
	return folderName;
}

function loadSampleFromParams() {
	var paramString = window.location.search || "sample='01_initialization/01_basic_init.html'";

	var folder = isFolder();
	if (folder) {
		var sample = document.querySelector("[data-folder='" + folder + "']");
		if (sample) {
			sample.click();
		}
	}


	var params = paramString.split("&");
	params.forEach(function(parameter){
		if (parameter.indexOf("filter=") > -1 ){
			var filter = decodeURI(parameter.split("filter=")[1]).replace(/"/g,"").replace(/'/g,"");
			document.querySelector(".search-field").value = filter;
			filterSamples(filter);
		}
		if (parameter.indexOf("sample=") > -1 ){
			var link = decodeURI(parameter.split("sample=")[1]).replace(/"/g,"").replace(/'/g,"");
			var path = link.split("/");
			if (path[0] == '.'){
				path[0] = document.querySelector("[data-folder]").dataset.folder;
			}
			var sample = document.querySelector("[data-folder='" + path[0] + "'][id='" + path[1] + "']");

			if (sample) {
				sample.click();
				setTimeout(function () {
					sample.parentNode.parentNode.querySelector("input").checked = true;
					var menu = document.querySelector(".links");
					var offset = menu.getBoundingClientRect().top;
					var folderCoordinates = sample.parentNode.parentNode.getBoundingClientRect();
					menu.scrollTo(0, folderCoordinates.y - offset);
				}, 4)
			}
		}
	});
}

function shareSample(){
	removeShareDialog();

	var currentUrl = window.location.protocol + "//" + window.location.host + window.location.pathname,
		currentSample = document.querySelector("#current_sample"),
		sample = currentSample.attributes.href.value,
		filter = document.querySelector(".search-field").value,
		link = currentUrl + "?sample='" + sample + "'&filter='" + filter + "'";

	var shareElement = document.createElement("div");
	shareElement.className = "share_dialog";

	var shareElementInside = document.createElement("div");
	shareElementInside.className = "share_dialog-field";

	var shareText = document.createElement("div");
	shareText.className = "share_text";
	shareText.innerHTML = "Copy the link: ";

	shareElement.appendChild(shareText);

	var shareLink = document.createElement("input");
	shareLink.className = "share_link";
	shareLink.value = link;
	shareElementInside.appendChild(shareLink);

	var shareButton = document.createElement("input");
	shareButton.className = "share_button";
	shareButton.type = "button";
	shareButton.value = "Copy";

	shareButton.onclick = function(){
		shareLink.select();
		document.execCommand('copy');
		removeShareDialog();
	};

	shareElementInside.appendChild(shareButton);
	shareElement.appendChild(shareElementInside);

	document.body.appendChild(shareElement);
}

function removeShareDialog(){
	var shareElement = document.querySelector(".share_dialog");

	if (shareElement){
		shareElement.innerHTML = '';
		shareElement.parentNode.removeChild(shareElement);
	}
}

function navDropdown(){
	var navDropdownElement = document.getElementById("nav-dropdown-list");

	navDropdownElement.classList.toggle("opened");
}

function toggle_dropdown(e){
	navDropdown();

	document.getElementById("nav-dropdown-chosen").innerText = e.target.innerText;
}
