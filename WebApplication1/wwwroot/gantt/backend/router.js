const actions = {
	inserted: "inserted",
	updated: "updated",
	deleted: "deleted",
	error: "error"
};

class Router {
	constructor(root, storage) {
		this.root = root;
		this.storage = storage;
	}

	_tryProcess(code, req, res) {
		try {
			code.call(this, req, res);
		} catch (e) {
			res.send({action: actions.error, message: e.message});
		}
	}

	getData(req, res) {
		res.send(this.storage.all());
	}

	insertTask(req, res) {
		this._tryProcess(function (req, res) {
			var insertedTask = this.storage.insert("data", req.body);
			res.send({action: actions.inserted, tid: insertedTask.id});
		}, req, res);

	}

	updateTask(req, res) {
		this._tryProcess(function (req, res) {
			var sid = req.params.id;

			this.storage.update(sid, "data", req.body);
			res.send({action: actions.updated});
		}, req, res)
	}

	deleteTask(req, res) {
		this._tryProcess(function (req, res) {
			var sid = req.params.id;
			this.storage.delete(sid, "data");
			res.send({action: actions.deleted});
		}, req, res);
	}

	insertLink(req, res) {
		this._tryProcess(function (req, res) {
			var insertedLink = this.storage.insert("links", req.body);
			res.send({action: actions.inserted, tid: insertedLink.id});
		}, req, res);
	}

	updateLink(req, res) {
		this._tryProcess(function (req, res) {
			var sid = req.params.id;
			this.storage.update(sid, "links", req.body);
			res.send({action: actions.updated});
		}, req, res);

	}

	deleteLink(req, res) {
		this._tryProcess(function (req, res) {
			var sid = req.params.id;
			this.storage.delete(sid, "links");
			res.send({action: actions.deleted});
		}, req, res);

	}

	connect(app) {
		this._connect(app, "");
		this._connect(app, "/gantt/backend");
		this._connect(app, "/backend");
	}

	_connect(app, prefix){
		app.get(`${prefix}${this.root}`, this.getData.bind(this));
		app.post(`${prefix}${this.root}/task`, this.insertTask.bind(this));
		app.put(`${prefix}${this.root}/task/:id`, this.updateTask.bind(this));
		app.delete(`${prefix}${this.root}/task/:id`, this.deleteTask.bind(this));
		app.post(`${prefix}${this.root}/link`, this.insertLink.bind(this));
		app.put(`${prefix}${this.root}/link/:id`, this.updateLink.bind(this));
		app.delete(`${prefix}${this.root}/link/:id`, this.deleteLink.bind(this));
	}
}
module.exports = Router;
