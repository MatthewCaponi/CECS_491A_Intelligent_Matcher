
  
  describe('Testing Leave Channel', () => {
    context('single value', () => {
      it('Leave Channel', () => {
        cy.visit(global.urlRoute + 'Messaging')

        cy.get('.search').select('2').contains('Richards Group')
        cy.wait(5000)

        cy.get('.ten > :nth-child(1) > :nth-child(2)').click()

        cy.wait(1000)

        cy.get('.search').contains('Richards Group').should('not.exist')


      })
    })
  })