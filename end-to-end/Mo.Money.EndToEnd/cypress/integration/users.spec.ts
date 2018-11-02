context('Users', () => {
    beforeEach(() => {
        cy.visit('/users');
    });
    
    it('should login to application', () => {
        cy.get('[data-cy=username]')
            .type('Admin');
        cy.get('[data-cy=password]')
            .type('CSrxQnlyJkucnhML4XcK2w==');
        cy.get('[data-cy=submit]')
            .click();
        
        cy.get('[data-cy=welcome]')
            .should('have.text', 'Welcome to Mo Money');
    })
});