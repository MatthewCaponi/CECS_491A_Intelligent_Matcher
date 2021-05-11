
  
  describe('Testing Create Channel', () => {
    context('single value', () => {
      it('Create and Delete Channel', () => {
        cy.visit(global.urlRoute + 'Messaging')


        cy.get('.action > input').type(`Test Channel`)

        cy.wait(5000)
        cy.get('.ten > :nth-child(1) > .button').click()
        cy.wait(2000)

        cy.get('.search').select('3').contains('Test Channel')

        cy.wait(2000)

        cy.get('.ten > :nth-child(1) > :nth-child(2)').click()

        cy.wait(2000)

        cy.get('.search').contains('Test Channel').should('not.exist')
      })
    })
  })