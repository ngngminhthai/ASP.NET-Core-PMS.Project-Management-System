/**
 * flatOwl
 * scrollSidebar
 * Scrollbar Message
 * Scrollbar MessageBox
 * sendMessage


 */

;
(function($) {

    "use strict";

    var isMobile = {
        Android: function() {
            return navigator.userAgent.match(/Android/i);
        },
        BlackBerry: function() {
            return navigator.userAgent.match(/BlackBerry/i);
        },
        iOS: function() {
            return navigator.userAgent.match(/iPhone|iPad|iPod/i);
        },
        Opera: function() {
            return navigator.userAgent.match(/Opera Mini/i);
        },
        Windows: function() {
            return navigator.userAgent.match(/IEMobile/i);
        },
        any: function() {
            return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
        }
    };

    var themesflatTheme = {

        // Main init function
        init: function() {
            this.config();
            this.events();
        },

        // Define vars for caching
        config: function() {
            this.config = {
                $window: $(window),
                $document: $(document),
            };
        },

        // Events
        events: function() {
            var self = this;

            // Run on document ready
            self.config.$document.on('ready', function() {

                // Retina Logos
                self.retinaLogo();

            });

            // Run on Window Load
            self.config.$window.on('load', function() {

            });
        },


        // Retina Logos
        retinaLogo: function() {
            var retina = window.devicePixelRatio > 1 ? true : false;
            var $logo = $('.logo-details img');
            var $logo_retina = $logo.data('retina');

            if (retina && $logo_retina) {
                $logo.attr({
                    src: $logo.data('retina'),
                    width: $logo.data('width'),
                    height: $logo.data('height')
                });
            }
        },



    }; // end themesflatTheme

    // Start things up
    themesflatTheme.init();



    var flatOwl = function() {
        if ($().owlCarousel) {
            $('.themesflat-carousel-box').each(function() {
                var
                    $this = $(this),
                    auto = $this.data("auto"),
                    item = $this.data("column"),
                    item2 = $this.data("column2"),
                    item3 = $this.data("column3"),
                    gap = Number($this.data("gap"));

                $this.find('.owl-carousel').owlCarousel({
                    margin: gap,
                    nav: true,
                    navigation: true,
                    pagination: true,
                    autoplay: false,
                    autoplayTimeout: 5000,
                    responsive: {
                        0: {
                            items: item3
                        },
                        600: {
                            items: item2
                        },
                        1000: {
                            items: item
                        }
                    }
                });
            });
        }
    };



    var scrollbarMessage = function() {
        if ($().mCustomScrollbar) {
            $(".box-message .box-content .scroll").mCustomScrollbar({
                scrollInertia: 400,
            });
        }
    }; // Scrollbar Message

    var scrollbarMessageBox = function() {
        if ($().mCustomScrollbar) {
            $("#message .message-info .scroll").mCustomScrollbar({
                scrollInertia: 400,
            });
        }
    }; // Scrollbar MessageBox





   /* var sendMessage = function() {
        $('textarea[name="message"]').each(function() {
            var text = $('textarea[name="message"]');
            $('.btn-send button').on('click', function(e) {
                if (text.val() == '') {
                    alert('Please type in the box to chat!');
                } else {
                    $('<div class="clearfix"></div><div class="message-in"><div class="message-pic"><img src="./images/avatar/message-1.png" alt=""><div class="pulse-css-1"></div></div><div class="message-body"><div class="message-text"><p>' + text.val() + '</p></div><div class="message-meta"><p>Sunday, march 17, 2021 at 2:59 PM</p></div></div></div>').appendTo('div.message-box');
                    text.val('');
                    var heights = $('div.message-box').height(),
                        agv = heights - 644;
                    $('div.message-box').css({
                        top: -(agv),
                    });
                };
                e.preventDefault();
            });
            $(this).keyup(function(event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    if (text.val() == '') {
                        alert('Please type in the box to chat!');
                    } else {
                        $('<div class="clearfix"></div><div class="message-in"><div class="message-pic"><img src="images/avatar/message-1.png" alt=""><div class="pulse-css-1"></div></div><div class="message-body"><div class="message-text"><p>' + text.val() + '</p></div><div class="message-meta"><p>Sunday, march 17, 2021 at 2:59 PM</p></div></div></div>').appendTo('div.message-box');
                        text.val('');
                        var heights = $('div.message-box').height(),
                            agv = heights - 644;
                        $('div.message-box').css({
                            top: -(agv),
                        });
                    };
                };
                event.preventDefault();
            });
        });
    }; // Send Message*/

    var flatCounter = function() {
        if ($().countTo) {
            $('.themesflat-counter').on('on-appear', function() {
                $(this).find('.number').each(function() {
                    var to = $(this).data('to'),
                        speed = $(this).data('speed');

                    $(this).countTo({
                        to: to,
                        speed: speed
                    });
                });
            });
        }
    };

    // Dom Ready

    $(function() {
        // scrollSidebar();
        flatOwl();
        scrollbarMessage();
        scrollbarMessageBox();
        // scrollbarTable();
        //sendMessage();
        flatCounter();
    });

})(jQuery);