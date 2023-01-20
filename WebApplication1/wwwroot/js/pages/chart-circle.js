// ______________ Chart-circle
if ($('.chart-circle').length) {
    $('.chart-circle').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: '#e5e9f2',
            lineCap: 'round'
        });
    });
}
// ______________ Chart-circle
if ($('.chart-circle-primary').length) {
    $('.chart-circle-primary').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: 'rgba(51, 102, 255, 0.4)',
            lineCap: 'round'
        });
    });
}

// ______________ Chart-circle
if ($('.chart-circle-secondary').length) {
    $('.chart-circle-secondary').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: 'rgba(254, 127, 0, 0.4)',
            lineCap: 'round'
        });
    });
}

// ______________ Chart-circle
if ($('.chart-circle-success').length) {
    $('.chart-circle-success').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: 'rgba(13, 205, 148, 0.5)',
            lineCap: 'round'
        });
    });
}

// ______________ Chart-circle
if ($('.chart-circle-warning').length) {
    $('.chart-circle-warning').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: 'rgba(247, 40, 74, 0.4)',
            lineCap: 'round'
        });
    });
}

// ______________ Chart-circle
if ($('.chart-circle-danger').length) {
    $('.chart-circle-danger').each(function() {
        let $this = $(this);

        $this.circleProgress({
            fill: {
                color: $this.attr('data-color')
            },
            size: $this.height(),
            startAngle: -Math.PI / 4 * 2,
            emptyFill: 'rgba(247, 40, 74, 0.4)',
            lineCap: 'round'
        });
    });
}