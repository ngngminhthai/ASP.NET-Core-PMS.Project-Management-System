const backend = require("./server");

const host = process.env.HOST || "127.0.0.1";
const port = process.env.PORT || "9200";

backend(host, port);