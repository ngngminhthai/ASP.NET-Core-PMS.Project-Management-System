var Storage = require("./storage");
class StorageTree extends Storage{
	children(parentId = 0) {
		var all = this._data.data;

		var loaded = this._data.data.filter(function(entry) {
			return parentId == entry.parent;
		});

		var search = {};
		loaded.forEach(function(value){
			search[value.id] = value;
		});

		all.forEach(function(task){
			if(search[task.parent]) {
				search[task.parent]["$has_child"] = true;
			}
		});

		var links = this._data.links.filter(function(entry) {
			return (search[entry.source] || search[entry.target]);
		});

		return {data: loaded, links: links};
	}
}

module.exports = StorageTree;