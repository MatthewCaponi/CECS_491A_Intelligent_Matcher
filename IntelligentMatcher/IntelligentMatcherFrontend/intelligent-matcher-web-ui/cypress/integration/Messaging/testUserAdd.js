
  
  describe('Testing User Add and Remove', () => {
    context('single value', () => {
      it('Add New user', () => {
        cy.visit('http://localhost:3000/Messaging')

        cy.get('.search').select('1').contains('Jakes Group')
        cy.wait(5000)

        cy.get('.three > :nth-child(1) > .input > input').type(`TestUser21`)
        
        cy.wait(500)

        cy.get(':nth-child(1) > .fluid').click()
        cy.wait(500)


        cy.get('.box').contains('TestUser21')
        cy.wait(500)


        cy.get(':nth-child(20) > :nth-child(2) > a').click()
        cy.wait(500)

        cy.get('.row > .three').contains('TestUser21').should('not.exist')

      })
    })
  })