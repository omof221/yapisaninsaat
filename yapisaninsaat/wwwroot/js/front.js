// TOAST
function showToast(message) {
    const toast = document.getElementById('toast');
    if (!toast) return;
    toast.textContent = message;
    toast.classList.add('show');
    setTimeout(() => { toast.classList.remove('show'); }, 2000);
}

// FAQ
function toggleFaq(element) {
    element.classList.toggle('closed');
    const icon = element.querySelector('.faq-icon iconify-icon');
    if (icon) {
        icon.setAttribute('icon', element.classList.contains('closed') ? 'lucide:plus' : 'lucide:minus');
    }
}

// SLIDER
document.addEventListener('DOMContentLoaded', function () {
    const track = document.getElementById('sliderTrack');
    const prevBtn = document.getElementById('sliderPrevBtn');
    const nextBtn = document.getElementById('sliderNextBtn');

  if (!track || !prevBtn || !nextBtn) return;

    let currentIndex = 0;
    const items = track.children;
    const totalItems = items.length;
    const maxIndex = Math.max(0, totalItems - 1);

    function updateSlider(animate) {
        if (animate === false) {
        track.style.transition = 'none';
  } else {
            track.style.transition = 'transform 0.5s cubic-bezier(0.25, 0.46, 0.45, 0.94)';
        }
        currentIndex = Math.min(Math.max(currentIndex, 0), maxIndex);
        track.style.transform = `translateX(${-(currentIndex * 100)}%)`;
        prevBtn.disabled = currentIndex === 0;
        nextBtn.disabled = currentIndex === maxIndex;
    }

    prevBtn.addEventListener('click', () => { currentIndex--; updateSlider(); });
    nextBtn.addEventListener('click', () => { currentIndex++; updateSlider(); });

    // Touch / Swipe
    let touchStartX = 0, touchEndX = 0, isSwiping = false;
    track.addEventListener('touchstart', (e) => { touchStartX = e.changedTouches[0].screenX; isSwiping = true; }, { passive: true });
    track.addEventListener('touchmove', (e) => { if (isSwiping) touchEndX = e.changedTouches[0].screenX; }, { passive: true });
    track.addEventListener('touchend', () => {
        if (!isSwiping) return;
        isSwiping = false;
        const diff = touchStartX - touchEndX;
      if (Math.abs(diff) > 50) {
if (diff > 0 && currentIndex < maxIndex) currentIndex++;
            else if (diff < 0 && currentIndex > 0) currentIndex--;
  updateSlider();
        }
    }, { passive: true });

    let resizeTimer;
    window.addEventListener('resize', () => {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(() => {
       updateSlider(false);
        requestAnimationFrame(() => {
                track.style.transition = 'transform 0.5s cubic-bezier(0.25, 0.46, 0.45, 0.94)';
   });
        }, 100);
    });

    updateSlider();
});
