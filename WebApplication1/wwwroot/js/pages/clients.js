function liststyle() {
    $('.list').addClass('active');
    $('.main-content.client').addClass('list');
    $('.list-board').removeClass('active');
}


function listboard() {
    $('.list-board').addClass('active');
    $('.main-content.client').removeClass('list');
    $('.list').removeClass('active');
}