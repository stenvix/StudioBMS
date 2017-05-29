// Write your Javascript code.

$(document).ready(function() {
    $(".chosen").chosen({
        inherit_select_classes: true,
        disable_search_threshold: 10,
        display_disabled_options: false,
        display_selected_options: false,
        max_selected_options: 4,
        no_results_text: "Оопс, нічого не знайдено!",
        placeholder_text_multiple: "Виберіть декілька позицій",
        placeholder_text_single: "Виберіть дані"
    });
})