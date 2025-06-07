document.addEventListener('DOMContentLoaded', () => {
    initializeQuiz();
});

function initializeQuiz() {
    updateProgressBar();
    addOptionClickHandlers();
    handleNavigationButtons();
    document.querySelector('.quiz-container').classList.add('fade-in');
}

function updateProgressBar() {
    const progressBar = document.querySelector('.progress-fill');
    if (!progressBar) return;

    const totalQuestions = document.querySelectorAll('.question-container').length;
    const currentQuestion = getCurrentQuestionIndex();
    const progress = (currentQuestion / totalQuestions) * 100;
    
    progressBar.style.width = `${progress}%`;
}

function getCurrentQuestionIndex() {
    const activeQuestion = document.querySelector('.question-container.active');
    if (!activeQuestion) return 0;

    const questions = document.querySelectorAll('.question-container');
    return Array.from(questions).indexOf(activeQuestion) + 1;
}

function addOptionClickHandlers() {
    const options = document.querySelectorAll('.option-button');
    options.forEach(option => {
        option.addEventListener('click', () => {
            const questionContainer = option.closest('.question-container');
            const allOptions = questionContainer.querySelectorAll('.option-button');
            
            allOptions.forEach(opt => opt.classList.remove('selected'));
            option.classList.add('selected');
            
            showToast('Answer selected!');
        });
    });
}

function handleNavigationButtons() {
    const prevButton = document.querySelector('.nav-button.prev');
    const nextButton = document.querySelector('.nav-button.next');
    
    if (prevButton) {
        prevButton.addEventListener('click', () => {
            navigateQuestions('prev');
        });
    }
    
    if (nextButton) {
        nextButton.addEventListener('click', () => {
            navigateQuestions('next');
        });
    }
}

function navigateQuestions(direction) {
    const questions = document.querySelectorAll('.question-container');
    const currentQuestion = document.querySelector('.question-container.active');
    const currentIndex = Array.from(questions).indexOf(currentQuestion);
    
    let nextIndex;
    if (direction === 'next') {
        nextIndex = currentIndex + 1;
    } else {
        nextIndex = currentIndex - 1;
    }
    
    if (nextIndex >= 0 && nextIndex < questions.length) {
        questions.forEach(q => {
            q.classList.remove('active');
            q.style.display = 'none';
        });
        
        questions[nextIndex].classList.add('active');
        questions[nextIndex].style.display = 'block';
        questions[nextIndex].classList.add('fade-in');
        
        updateProgressBar();
        updateNavigationButtons(nextIndex, questions.length);
    }
}

function updateNavigationButtons(currentIndex, totalQuestions) {
    const prevButton = document.querySelector('.nav-button.prev');
    const nextButton = document.querySelector('.nav-button.next');
    
    if (prevButton) {
        prevButton.disabled = currentIndex === 0;
    }
    
    if (nextButton) {
        const isLastQuestion = currentIndex === totalQuestions - 1;
        nextButton.textContent = isLastQuestion ? 'Submit' : 'Next';
        
        if (isLastQuestion) {
            nextButton.addEventListener('click', submitQuiz, { once: true });
        }
    }
}

function submitQuiz() {
    const selectedAnswers = [];
    const questions = document.querySelectorAll('.question-container');
    
    questions.forEach(question => {
        const selectedOption = question.querySelector('.option-button.selected');
        selectedAnswers.push(selectedOption ? selectedOption.dataset.value : null);
    });
    
    // Submit answers to the server
    fetch('/api/quiz/submit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ answers: selectedAnswers })
    })
    .then(response => response.json())
    .then(result => {
        showResults(result);
    })
    .catch(error => {
        console.error('Error submitting quiz:', error);
        showToast('Error submitting quiz. Please try again.');
    });
}

function showResults(result) {
    const quizContainer = document.querySelector('.quiz-container');
    quizContainer.innerHTML = `
        <div class="score-container fade-in">
            <h2 class="score-title">Quiz Complete!</h2>
            <div class="score-value">${result.score}%</div>
            <p class="feedback-message">${getFeedbackMessage(result.score)}</p>
            <button class="nav-button" onclick="location.reload()">Try Again</button>
        </div>
    `;
}

function getFeedbackMessage(score) {
    if (score >= 90) return "Excellent! You're a quiz master! 🏆";
    if (score >= 70) return "Great job! You've done very well! 👏";
    if (score >= 50) return "Good effort! Keep practicing! 💪";
    return "Don't worry! Practice makes perfect! 📚";
}

function showToast(message) {
    const existingToast = document.querySelector('.toast');
    if (existingToast) {
        existingToast.remove();
    }
    
    const toast = document.createElement('div');
    toast.className = 'toast';
    toast.textContent = message;
    document.body.appendChild(toast);
    
    // Trigger reflow
    toast.offsetHeight;
    
    toast.classList.add('show');
    
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 300);
    }, 3000);
} 