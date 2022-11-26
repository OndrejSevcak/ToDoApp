function displaySection(sectionName){
    let allSections = document.getElementsByTagName('section');
    let sectionsArray = Array.prototype.slice.call(allSections);

    sectionsArray.forEach(element => {
        element.style.display = 'none';
    });
    
    let section = document.getElementById(sectionName);
    section.style.display = 'block';
};