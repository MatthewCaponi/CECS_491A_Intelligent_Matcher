
  describe('Testing Message Send and Delete', () => {
    context('single value', () => {
      it('Send Test Message', () => {
        cy.visit('http://localhost:3000/Messaging')

        cy.get('.search').select('1').contains('Jakes Group')
        cy.wait(5000)

        cy.get('.fluid > input').type(`Sending Test Message`)
        
        cy.wait(500)

        cy.get('.fluid > .ui').click()
        cy.wait(500)


        cy.get('.box').contains('Sending Test Message')


        cy.get(':nth-child(3) > div > :nth-child(2) > td > a > span').click()

        cy.get('.box').contains('Sending Test Message').should('not.exist')

      })
    })
  })