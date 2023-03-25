var Router = require("./router");

class RouterDynamicLoading extends Router {
	getData(req, res) {
		this._tryProcess(function (req, res) {
			var parentId = req.query.parent_id || 0;
			res.send(this.storage.children(parentId));
		}, req, res);
	}
}

module.exports = RouterDynamicLoading;