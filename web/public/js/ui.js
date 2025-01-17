export function openProfileSmallMenu(){
    const extraButtons = document.getElementById('extraButtons');
    if (extraButtons.classList.contains('hidden')) {
        extraButtons.classList.remove('hidden');
        extraButtons.css("display","block");
    } else {
        extraButtons.classList.add('hidden');
    }
}