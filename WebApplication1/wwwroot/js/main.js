// Dark light version
const themeCookieName = 'theme'
const themeDark = 'dark'
const themeLight = 'light'

const body = document.getElementsByTagName('body')[0]

function setCookie(cname, cvalue, exdays) {
    var d = new Date()
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000))
    var expires = "expires=" + d.toUTCString()
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/"
}

function getCookie(cname) {
    var name = cname + "="
    var ca = document.cookie.split(';')
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1)
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length)
        }
    }
    return ""
}

loadTheme()

function loadTheme() {
    var theme = getCookie(themeCookieName)
    body.classList.add(theme === "" ? themeLight : theme)
}

function switchTheme() {

    if (body.classList.contains(themeLight)) {
        body.classList.remove(themeLight)
        body.classList.add(themeDark)
        setCookie(themeCookieName, themeDark)

    } else {
        body.classList.remove(themeDark)
        body.classList.add(themeLight)
        setCookie(themeCookieName, themeLight)
    }
}

// Sidebar Menu

document.querySelectorAll('.sidebar-submenu').forEach(e => {
    e.querySelector('.sidebar-menu-dropdown').onclick = (event) => {
        event.preventDefault()
        e.querySelector('.sidebar-menu-dropdown .dropdown-icon').classList.toggle('active')

        let dropdown_content = e.querySelector('.sidebar-menu-dropdown-content')
        let dropdown_content_lis = dropdown_content.querySelectorAll('li')

        let active_height = dropdown_content_lis[0].clientHeight * dropdown_content_lis.length

        dropdown_content.classList.toggle('active')

        dropdown_content.style.height = dropdown_content.classList.contains('active') ? active_height + 'px' : '0'
    }
})


let overlay = document.querySelector('.overlay')
let sidebar = document.querySelector('.sidebar')
let sidebar_expand = document.querySelector('.sidebar-expand')

document.querySelector('#mobile-toggle').onclick = () => {

    sidebar_expand.classList.toggle('active')
    overlay.classList.toggle('active')
}

document.querySelector('#sidebar-close').onclick = () => {

    sidebar_expand.classList.toggle('active')
    overlay.classList.toggle('active')
}

$(function() {
    let path = window.location.href;
    let dropdown_content = document.querySelector('.sidebar-menu-dropdown-content')
    let dropdown_content_lis = dropdown_content.querySelectorAll('li')
    let active_height = dropdown_content_lis[0].clientHeight * dropdown_content_lis.length

    $(".sidebar-menu a").each(function() {
        if (this.href === path) {
            $(this).addClass("active");
            dropdown_content.style.height = dropdown_content.classList.contains('active') ? active_height + 'px' : '0'
        } else {
            $(this).removeClass("active");
        }
    });
});