export function openProfileSmallMenu(){
    const extraButtons = document.getElementById('extraButtons');
    if (extraButtons.classList.contains('hidden')) {
        extraButtons.classList.remove('hidden');
    } else {
        extraButtons.classList.add('hidden');
    }
}