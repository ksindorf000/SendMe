﻿$(document).ready(function () {
    $('#goal').goalProgress({
        goalAmount: targetAmount,
        currentAmount: currentTotal,
        textBefore: '$',
        textAfter: ' raised.'
    });
});

!function ($) {
    $.fn.extend({
        goalProgress: function (options) {
            var defaults = {
                goalAmount: 100,
                currentAmount: 50,
                speed: 500,
                textBefore: '',
                textAfter: '',
                milestoneNumber: 70,
                milestoneClass: 'almost-full',
                callback: function () { }
            }

            var options = $.extend(defaults, options);
            return this.each(function () {
                var obj = $(this);

                var goalAmountParsed = parseInt(defaults.goalAmount);
                var currentAmountParsed = parseInt(defaults.currentAmount);

                var percentage = (currentAmountParsed / goalAmountParsed) * 100;
                var milestoneNumberClass = (percentage > defaults.milestoneNumber) ? ' ' + defaults.milestoneClass : ''

                var progressBar = '<div class="progressBar">' + defaults.textBefore + currentAmountParsed + defaults.textAfter + '</div>';

                var progressBarWrapped = '<div class="goalProgress' + milestoneNumberClass + '">' + progressBar + '</div>';

                obj.append(progressBarWrapped);
                var rendered = obj.find('div.progressBar');
                rendered.each(function () {
                    $(this).html($(this).text().replace(/\s/g, '&nbsp;'));
                });

                rendered.animate({ width: percentage + '%' }, defaults.speed, defaults.callback);

                if (typeof callback == 'function') {
                    callback.call(this)
                }
            });
        }
    });
}(window.jQuery);