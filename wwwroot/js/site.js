// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Mobile menu toggle
const mobileMenuButton = document.getElementById('mobile-menu-button');
const mobileMenu = document.getElementById('mobile-menu');

if (mobileMenuButton && mobileMenu) {
  mobileMenuButton.addEventListener('click', (e) => {
    e.stopPropagation();
    mobileMenu.classList.toggle('open');
  });
}

// Close mobile menu when clicking outside
document.addEventListener('click', (e) => {
  if (!mobileMenu.contains(e.target) && e.target !== mobileMenuButton) {
    mobileMenu.classList.remove('open');
  }
});

// Categories dropdown toggle (desktop)
const categoriesButton = document.getElementById('categories-dropdown-button');
const categoriesMenu = document.getElementById('categories-dropdown-menu');
const categoriesChevron = document.getElementById('categories-chevron');

if (categoriesButton && categoriesMenu) {
  categoriesButton.addEventListener('click', (e) => {
    e.stopPropagation();
    categoriesMenu.classList.toggle('open');
    if (categoriesChevron) categoriesChevron.classList.toggle('open');
  });
}

// Categories dropdown toggle (mobile)
const mobileCategoriesButton = document.getElementById('mobile-categories-button');
const mobileCategoriesMenu = document.getElementById('mobile-categories-dropdown');
const mobileCategoriesChevron = document.getElementById('mobile-categories-chevron');

if (mobileCategoriesButton && mobileCategoriesMenu) {
  mobileCategoriesButton.addEventListener('click', (e) => {
    e.stopPropagation();
    mobileCategoriesMenu.classList.toggle('open');
    if (mobileCategoriesChevron) mobileCategoriesChevron.classList.toggle('open');
  });
}

// Profile dropdown toggle
const profileButton = document.getElementById('profile-dropdown-button');
const profileMenu = document.getElementById('profile-dropdown-menu');

if (profileButton && profileMenu) {
  profileButton.addEventListener('click', (e) => {
    e.stopPropagation();
    profileMenu.classList.toggle('open');
  });
}

// Close dropdowns when clicking outside
document.addEventListener('click', (e) => {
  if (categoriesMenu && !categoriesButton.contains(e.target) && !categoriesMenu.contains(e.target)) {
    categoriesMenu.classList.remove('open');
    if (categoriesChevron) categoriesChevron.classList.remove('open');
  }

  if (mobileCategoriesMenu && !mobileCategoriesButton.contains(e.target) && !mobileCategoriesMenu.contains(e.target)) {
    mobileCategoriesMenu.classList.remove('open');
    if (mobileCategoriesChevron) mobileCategoriesChevron.classList.remove('open');
  }

  if (profileMenu && profileButton && !profileButton.contains(e.target) && !profileMenu.contains(e.target)) {
    profileMenu.classList.remove('open');
  }
});

// Login/Signup modal functionality
const authContainer = document.getElementById('authContainer');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');
const modalOverlay = document.getElementById('modalOverlay');
const openModalBtn = document.getElementById('openModalBtn');
const closeModalBtn = document.getElementById('closeModalBtn');

// Function to open modal and reset to sign-in form
function openModal() {
  modalOverlay.style.display = 'flex';
  document.body.style.overflow = 'hidden';
  authContainer.classList.remove("active"); // Reset to sign-in form
}

// Function to close modal
function closeModal() {
  modalOverlay.style.display = 'none';
  document.body.style.overflow = 'auto';
}

if (registerBtn && loginBtn && authContainer) {
  registerBtn.addEventListener('click', (e) => {
    e.preventDefault();
    authContainer.classList.add("active");
  });

  loginBtn.addEventListener('click', (e) => {
    e.preventDefault();
    authContainer.classList.remove("active");
  });
}

if (openModalBtn && modalOverlay) {
  openModalBtn.addEventListener('click', (e) => {
    e.preventDefault();
    openModal();
  });
}

if (closeModalBtn && modalOverlay) {
  closeModalBtn.addEventListener('click', (e) => {
    e.preventDefault();
    closeModal();
  });

  modalOverlay.addEventListener('click', (e) => {
    if (e.target === modalOverlay) {
      closeModal();
    }
  });
}

// Close modal when pressing Escape key
document.addEventListener('keydown', (e) => {
  if (e.key === 'Escape' && modalOverlay.style.display === 'flex') {
    closeModal();
  }
});


$('#DashboardTable').length && $('#DashboardTable').DataTable();


