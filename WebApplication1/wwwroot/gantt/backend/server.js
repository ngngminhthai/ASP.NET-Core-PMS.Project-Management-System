var express = require("express");
var bodyParser = require("body-parser");
var path = require("path");
var Storage = require("./storage");
var StorageTree = require("./storage_tree");
var Router = require("./router");
var RouterDynamicLoading = require("./router_dynamic_loading");
var itemInitializer = require("./item_initializer");

module.exports = function (host = "127.0.0.1", port = "9200") {

	var app = express();

	var storage = new Storage(require("./data.json"), itemInitializer);
	var dynamicStorage = new StorageTree(require("./data-dynamic.json"), itemInitializer);
	var router = new Router("/data", storage);
	var dynLoading = new RouterDynamicLoading("/data-dynamic", dynamicStorage);

	app.use(bodyParser.json()); // for parsing application/json
	app.use(bodyParser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded
	app.use(function (req, res, next) {
		res.header("Access-Control-Allow-Origin", "*");
		res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
		res.header("Access-Control-Allow-Methods", "*");
		next();
	});

	router.connect(app);
	dynLoading.connect(app);

	app.use("/codebase", express.static(path.join(__dirname, "..", "codebase")));
	app.use("/samples", express.static(path.join(__dirname, "..", "samples")));
	app.use(/^\/$/, function (req, res) {//default url
		res.redirect("/samples");
	});

	var server = app.listen(port, host, function () {
		console.log("Server is running on port " + port + "...");
	});

};